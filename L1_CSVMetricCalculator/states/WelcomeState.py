from states.BaseState import BaseState


class WelcomeState(BaseState):
    final: bool
    context: "Program"
    action_table: dict

    def __init__(self, context: "Program") -> None:
        self.final = False
        self.context = context
        self.action_table = dict()

    def enter_path_action(self) -> None:
        self.context.set_state("enter_path_state")

    def quit_program_action(self) -> None:
        self.context.set_state("bye_state")

    def gen_actions(self) -> None:
        self.action_table["p"] = dict(description="select path",
                                       action=self.enter_path_action)
        self.action_table["q"] = dict(description="quit program",
                                      action=self.quit_program_action)

    def print_actions(self) -> None:
        print("List of available actions:")
        for key in self.action_table.keys():
            print(f"\t[{key}] - {self.action_table[key]['description']}")

    def choose_action(self) -> None:
        print("Hello. It is CSV Metric Calculator.")
        self.print_actions()

        while True:
            response = input("Choose action: ")

            if response in self.action_table.keys():
                self.action_table[response]["action"]()
                break
            else:
                print("Choose action from the list.")

    def restore_state(self) -> None:
        self.action_table.clear()

    def work(self) -> None:
        self.gen_actions()
        self.choose_action()
        self.restore_state()
