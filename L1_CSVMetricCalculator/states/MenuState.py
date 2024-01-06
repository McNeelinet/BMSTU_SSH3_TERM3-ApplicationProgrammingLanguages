from states.BaseState import BaseState


class MenuState(BaseState):
    final: bool
    context: "Program"
    settings_table: dict
    action_table: dict

    def __init__(self, context: "Program") -> None:
        self.final = False
        self.context = context
        self.action_table = dict()
        self.settings_table = dict()

    def enter_path_action(self) -> None:
        self.context.set_state("enter_path_state")

    def enter_region_action(self) -> None:
        self.context.set_state("enter_region_state")

    def enter_metric_action(self) -> None:
        self.context.set_state("enter_metric_state")

    def calculate_metric_state(self):
        self.context.set_state("calculate_metrics_state")

    def quit_program_action(self) -> None:
        self.context.set_state("bye_state")

    def gen_settings(self) -> None:
        self.settings_table["filled"] = dict()
        self.settings_table["unfilled"] = dict()

        for attr_name in ("path", "region", "metric"):
            attr = getattr(self.context, attr_name)
            if attr:
                self.settings_table["filled"][attr_name] = attr
            else:
                self.settings_table["unfilled"][attr_name] = attr

    def gen_actions(self) -> None:
        unfilled: list[str] = self.settings_table["unfilled"].keys()

        self.action_table["p"] = dict(description=f"{'select' if ('path' in unfilled) else 'change'} path",
                                      action=self.enter_path_action)
        self.action_table["r"] = dict(description=f"{'select' if ('region' in unfilled) else 'change'} region",
                                      action=self.enter_region_action)
        self.action_table["m"] = dict(description=f"{'select' if ('metric' in unfilled) else 'change'} metric",
                                      action=self.enter_metric_action)
        self.action_table["q"] = dict(description="quit program",
                                      action=self.quit_program_action)
        if not unfilled:
            self.action_table["calc"] = dict(description="calc metrics",
                                             action=self.calculate_metric_state)

    def print_settings(self) -> None:
        print("Here are your settings:")
        for key in self.settings_table["filled"].keys():
            print(f"\t{key.capitalize()}: {self.settings_table['filled'][key]}")

        if self.settings_table["unfilled"]:
            print("Before you can calc metrics you must fill:")
            for key in self.settings_table["unfilled"].keys():
                print(f"\t{key.capitalize()}")

    def print_actions(self) -> None:
        print("List of available actions:")
        for key in self.action_table.keys():
            print(f"\t[{key}] - {self.action_table[key]['description']}")

    def choose_action(self) -> None:
        self.print_settings()
        self.print_actions()

        while True:
            response = input("Choose action: ")

            if response in self.action_table.keys():
                self.action_table[response]["action"]()
                break
            else:
                print("Choose action from the list.")

    def restore_state(self) -> None:
        self.settings_table.clear()
        self.action_table.clear()

    def work(self) -> None:
        self.gen_settings()
        self.gen_actions()
        self.choose_action()
        self.restore_state()
