from abc import ABC, abstractmethod


class BaseState(ABC):
    final: bool

    def is_final(self) -> bool:
        return self.final

    @abstractmethod
    def work(self) -> None:
        pass
