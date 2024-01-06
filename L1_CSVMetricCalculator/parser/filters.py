from exceptions.UnknownPropertyException import UnknownPropertyException


def pattern_match_filter(kw_line: dict, pattern: dict):
    try:
        for k in pattern.keys():
            if kw_line[k] != pattern[k]:
                return False
    except KeyError:
        raise UnknownPropertyException
    return True
