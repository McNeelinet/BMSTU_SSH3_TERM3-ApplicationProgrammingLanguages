from states.BaseState import BaseState
from states.WelcomeState import WelcomeState
from states.ByeState import ByeState
from states.EnterPathState import EnterPathState
from states.MenuState import MenuState
from states.EnterRegionState import EnterRegionState
from states.EnterMetricState import EnterMetricState
from states.CalculateMetricsState import CalculateMetricsState
from states.CheckFileState import CheckFileState
from exceptions import UnknownStateException


class Program:
    state_table: dict[str, BaseState]
    current_state: BaseState

    def __init__(self) -> None:
        self.state_table = {
            "welcome_state": WelcomeState(self),
            "bye_state": ByeState(self),
            "enter_path_state": EnterPathState(self),
            "check_file_state": CheckFileState(self),
            "menu_state": MenuState(self),
            "enter_region_state": EnterRegionState(self),
            "enter_metric_state": EnterMetricState(self),
            "calculate_metrics_state": CalculateMetricsState(self),
        }

        self.set_state("welcome_state")
        self.path = None
        self.region = None
        self.metric = None

    def reset_settings(self) -> None:
        self.path = None
        self.region = None
        self.metric = None

    def set_state(self, state: str) -> None:
        if state in self.state_table.keys():
            self.current_state = self.state_table[state]
        else:
            raise UnknownStateException

    def work(self) -> None:
        while not self.current_state.is_final():
            self.current_state.work()
            print()
        self.current_state.work()

    @property
    def path(self) -> str:
        return self._path

    @path.setter
    def path(self, path: str | None) -> None:
        self._path = path

    @property
    def region(self) -> str:
        return self._region

    @region.setter
    def region(self, region: str | None) -> None:
        self._region = region

    @property
    def column(self) -> str:
        return self._column

    @column.setter
    def column(self, column: str | None) -> None:
        self._column = column
