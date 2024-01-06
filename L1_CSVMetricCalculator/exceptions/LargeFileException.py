class LargeFileException(Exception):
    def __init__(self, msg="Selected file is too large. "
                           "Choose another one."):
        super().__init__(msg)
