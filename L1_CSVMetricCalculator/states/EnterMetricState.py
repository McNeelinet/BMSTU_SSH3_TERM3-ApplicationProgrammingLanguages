from states.BaseState import BaseState
from parser.CSVParser import CSVParser
from exceptions.EmptyFileException import EmptyFileException


class EnterMetricState(BaseState):
    final: bool
    context: "Program"
    metrics_list: list

    def __init__(self, context: "Program") -> None:
        self.final = False
        self.context = context
        self.metrics_list = list()

    def gen_columns_list(self) -> None:
        with open(self.context.path) as file:
            parser = CSVParser(file)

            out = parser.get_titles()
            self.metrics_list.extend(out)

    def print_columns_list(self):
        print("Please, select metric from the list below:")
        for i in range(len(self.metrics_list)):
            print(f"\t[{i}] - {self.metrics_list[i]}")

    def choose_column(self) -> None:
        self.print_columns_list()

        while True:
            response = input("Metric: ")

            if response.isnumeric() and int(response) in range(len(self.metrics_list)):
                self.context.metric = self.metrics_list[int(response)]
                break
            else:
                print("Select metric from the list. Enter number.")

    def restore(self) -> None:
        self.metrics_list.clear()

    def work(self) -> None:
        try:
            self.gen_columns_list()
            self.choose_column()
            self.context.set_state("menu_state")
        except EmptyFileException as error:
            print(error)
            self.context.reset_settings()
            self.context.set_state("enter_path_state")
        except FileNotFoundError:
            print("It looks that selected file is no longer exists. Choose another one.")
            self.context.reset_settings()
            self.context.set_state("enter_path_state")

        self.restore()
