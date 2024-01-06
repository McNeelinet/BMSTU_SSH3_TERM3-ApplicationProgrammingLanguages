import math


class MetricCalculator:

    def is_float(self, element: str):
        try:
            float(element)
            return True
        except ValueError:
            return False

    def calc_maximum(self, numeric_data: list[float]) -> float:

        return max(numeric_data)

    def calc_minimum(self, numeric_data: list[float]) -> float:

        return min(numeric_data)

    def calc_average(self, numeric_data: list[float]) -> float:

        return sum(numeric_data) / len(numeric_data)

    def calc_median(self, numeric_data: list[float]) -> float:
        numeric_data.sort()

        n = len(numeric_data)
        if n % 2 == 0:
            median = (numeric_data[n // 2 - 1] + numeric_data[n // 2]) / 2
        else:
            median = numeric_data[n // 2]

        return median

    def calc_percentile(self, numeric_data: list[float], percentile: float):
        if percentile <= 0:
            return min(numeric_data)
        elif percentile >= 1:
            return max(numeric_data)

        k = (len(numeric_data) - 1) * percentile
        f = math.floor(k)
        c = math.ceil(k)
        if f == c:
            return numeric_data[int(k)]
        d0 = numeric_data[int(f)] * (c - k)
        d1 = numeric_data[int(c)] * (k - f)

        return d0 + d1
