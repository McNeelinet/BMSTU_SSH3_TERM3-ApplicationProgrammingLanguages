from states.BaseState import BaseState
from parser.CSVParser import CSVParser
from exceptions.RegionColumnUnspecifiedException import RegionColumnUnspecifiedException


class EnterRegionState(BaseState):
    final: bool
    context: "Program"
    regions_list: list

    def __init__(self, context: "Program") -> None:
        self.final = False
        self.context = context
        self.regions_list = list()

    def gen_regions_list(self):
        with open(self.context.path) as file:
            parser = CSVParser(file)

            out = parser.get_lines(select=("region",))
            regions = set([i["region"] for i in out if i["region"] not in (None, "")])
            self.regions_list.extend(regions)
            self.regions_list.sort()
        if not self.regions_list:
            raise RegionColumnUnspecifiedException

    def print_region_list(self):
        print("Please, select region from the list below:")
        for i in range(len(self.regions_list)):
            print(f"\t[{i}] - {self.regions_list[i]}")

    def choose_region(self) -> None:
        self.print_region_list()

        while True:
            response = input("Region: ")

            if response.isnumeric() and int(response) in range(len(self.regions_list)):
                self.context.region = self.regions_list[int(response)]
                break
            else:
                print("Select region from the list. Enter number.")

    def restore(self) -> None:
        self.regions_list.clear()

    def work(self) -> None:
        try:
            self.gen_regions_list()
            self.choose_region()
            self.context.set_state("menu_state")
        except RegionColumnUnspecifiedException:
            print("It looks like there is no region column in the selected file. Choose another one.")
            self.context.reset_settings()
            self.context.set_state("enter_path_state")
        except FileNotFoundError:
            print("It looks that selected file is no longer exists. Choose another one.")
            self.context.reset_settings()
            self.context.set_state("enter_path_state")
        self.restore()
