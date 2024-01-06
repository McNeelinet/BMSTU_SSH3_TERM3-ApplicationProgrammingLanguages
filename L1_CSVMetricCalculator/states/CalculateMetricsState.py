from states.BaseState import BaseState
from parser.CSVParser import CSVParser
from calculator.MetricCalculator import MetricCalculator


class CalculateMetricsState(BaseState):
    final: bool
    context: "Program"

    def __init__(self, context: "Program") -> None:
        self.final = False
        self.context = context

    def print_table(self, data) -> None:
        print("Table:")
        print(f" {'year': <8}| {self.context.metric}")
        for d in data:
            print(f" {d['year']: <8}| {d[self.context.metric]}")

    def print_calculations(self, data: list[str]):
        metric_calc = MetricCalculator()
        numeric_data = [float(d[self.context.metric]) for d in data if metric_calc.is_float(d[self.context.metric])]

        print("Calculations:")
        if numeric_data:
            print(f"Minimum: {metric_calc.calc_minimum(numeric_data)}")
            print(f"Maximum: {metric_calc.calc_maximum(numeric_data)}")
            print(f"Median: {metric_calc.calc_median(numeric_data)}")
            print(f"Average: {metric_calc.calc_average(numeric_data)}")
        else:
            print("There is nothing to calculate.")

    def print_percentile(self, data: list[str]):
        metric_calc = MetricCalculator()
        numeric_data = [float(d[self.context.metric]) for d in data if metric_calc.is_float(d[self.context.metric])]
        numeric_data = sorted(numeric_data)

        print("Percentile table:")
        if numeric_data:
            counter: float = 0
            while counter <= 1.01:
                print(f" {counter: <7.2f} | {round(metric_calc.calc_percentile(numeric_data, counter), 2)}")
                counter += 0.05
        else:
            print("There is nothing to calculate.")

    def print_everything(self):
        with open(self.context.path) as file:
            parser = CSVParser(file)
            data = parser.get_lines(select=("year", self.context.metric),
                                    pattern={"region": self.context.region})

            self.print_table(data)
            print()
            self.print_calculations(data)
            print()
            self.print_percentile(data)

    def work(self) -> None:
        self.print_everything()
        self.context.set_state("menu_state")
