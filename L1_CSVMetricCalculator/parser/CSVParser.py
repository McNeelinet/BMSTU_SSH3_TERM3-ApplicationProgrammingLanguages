from typing import TextIO
from parser.filters import pattern_match_filter


class CSVParser:
    file: TextIO

    def __init__(self, file: TextIO) -> None:
        self.file = file

    def get_line(self) -> list[str]:
        line: str = self.file.readline()
        if not line:
            return []

        line_values: list[str] = line.split(",") if line.strip() != "" else []
        line_values = [value.strip() for value in line_values]
        return line_values

    def get_titles(self) -> list[str]:
        current_pos: int = self.file.tell()
        self.file.seek(0)

        titles: list[str] = self.get_line()

        self.file.seek(current_pos)
        return titles

    def get_lines(self, **kwargs) -> list[dict[str:str]]:
        current_pos: int = self.file.tell()
        self.file.seek(0)

        titles: list[str] = self.get_line()
        kwargs.setdefault("select", titles)
        kwargs.setdefault("pattern", dict())

        result: list = list()
        while line := self.get_line():
            kw_line = dict(zip(titles, line))
            if pattern_match_filter(kw_line, kwargs["pattern"]):
                result.append({x: kw_line.get(x) for x in kwargs["select"]})

        self.file.seek(current_pos)
        return result
