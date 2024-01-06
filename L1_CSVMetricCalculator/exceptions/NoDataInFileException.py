class NoDataInFileException(Exception):
    def __init__(self, msg="Selected file contains only headers, without data. "
                           "Choose another one or fix this."):
        super().__init__(msg)
