from states.BaseState import BaseState
from exceptions.LargeFileException import LargeFileException
from exceptions.EmptyFileException import EmptyFileException
from exceptions.RegionColumnUnspecifiedException import RegionColumnUnspecifiedException
from exceptions.NoDataInFileException import NoDataInFileException
from exceptions.ColumnsValuesDifferentCountException import ColumnsValuesDifferentCountException
from os.path import getsize
from parser.CSVParser import CSVParser

max_size: int = 2000000000


class CheckFileState(BaseState):
    final: bool
    context: "Program"
    warning_table: dict[str:str]

    def __init__(self, context: "Program") -> None:
        self.final = False
        self.context = context
        self.warning_table = dict(region_unfilled=False,
                                  data_unfilled=False, )

    def check_content(self) -> None:
        if getsize(self.context.path) >= max_size:  # >= 2gb
            raise LargeFileException

        with open(self.context.path) as file:
            parser = CSVParser(file)
            titles: list[str] = parser.get_line()

            if not titles:
                raise EmptyFileException
            if not ("region" in titles):
                raise RegionColumnUnspecifiedException
            region_pos: int = titles.index("region")

            total_lines: int = 1
            while line := parser.get_line():
                total_lines += 1
                if len(titles) != len(line):
                    raise ColumnsValuesDifferentCountException

                if line[region_pos] == "":
                    self.warning_table["region_unfilled"] = True
                elif "" in line:
                    self.warning_table["data_unfilled"] = True

            if total_lines < 2:
                raise NoDataInFileException

    def print_warnings(self) -> None:
        if self.warning_table["region_unfilled"]:
            print("Attention: Selected file contains lines with unfilled region column. "
                  "Such lines will be ignored.")
        if self.warning_table["data_unfilled"]:
            print("Attention: Selected file contains lines with unfilled data. "
                  "Such lines will not be used in calculations.")

    def restore(self) -> None:
        self.warning_table = dict(region_unfilled=False,
                                  data_unfilled=False, )

    def work(self) -> None:
        try:
            self.check_content()
            self.print_warnings()
            self.context.set_state("menu_state")
        except (LargeFileException, EmptyFileException, RegionColumnUnspecifiedException,
                NoDataInFileException, ColumnsValuesDifferentCountException) as error:
            print(error)
            self.context.set_state("enter_path_state")
        finally:
            self.warning_table = dict(region_unfilled=False,
                                      data_unfilled=False, )
