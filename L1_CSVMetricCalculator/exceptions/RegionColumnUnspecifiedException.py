class RegionColumnUnspecifiedException(Exception):
    def __init__(self, msg="Region column in selected file is undefined. "
                           "Choose another one or fix this."):
        super().__init__(msg)
