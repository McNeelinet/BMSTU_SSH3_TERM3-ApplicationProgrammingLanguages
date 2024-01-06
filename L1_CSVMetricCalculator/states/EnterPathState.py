from os.path import isfile, splitext
from states.BaseState import BaseState


class EnterPathState(BaseState):
    final: bool
    context: "Program"

    def __init__(self, context: "Program") -> None:
        self.final = False
        self.context = context

    def choose_path(self) -> None:
        print("Please, enter path to .csv file.")

        while True:
            response = input("Path: ")
            if isfile(response) and splitext(response)[1] == ".csv":
                self.context.path = response
                break
            else:
                print("Enter valid path to .csv file.")

    def work(self) -> None:
        self.context.reset_settings()
        self.choose_path()
        self.context.set_state("check_file_state")
