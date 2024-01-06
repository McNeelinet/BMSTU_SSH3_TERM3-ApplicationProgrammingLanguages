class ColumnsValuesDifferentCountException(Exception):
    def __init__(self, msg="Number of columns and filled values in line is different in selected file. "
                           "Choose another one or fix this."):
        super().__init__(msg)
