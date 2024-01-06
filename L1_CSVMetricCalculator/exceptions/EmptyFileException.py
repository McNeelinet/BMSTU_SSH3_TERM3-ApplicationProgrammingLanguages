class EmptyFileException(Exception):
    def __init__(self, msg="Selected file is empty. "
                           "Choose another one or fix this."):
        super().__init__(msg)
