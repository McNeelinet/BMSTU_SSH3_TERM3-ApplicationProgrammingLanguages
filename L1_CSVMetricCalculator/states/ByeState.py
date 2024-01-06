from states.BaseState import BaseState


class ByeState(BaseState):
    final: bool
    context: "Program"

    def __init__(self, context: "Program") -> None:
        self.final = True
        self.context = context

    def work(self) -> None:
        print("Bye!")
