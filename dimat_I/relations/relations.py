from typing import Tuple, List, Set, Any


def is_irreflexive(relation):

    for x, y in relation:
        if x == y:
            return False
    return True


def is_relexive(relation: set) -> bool:
    return all((x, x) in relation for x in set([a for a, b in relation] + [b for a, b in relation]))


def is_symmetric(relation: set) -> bool:
    return all((b, a) in relation for a, b in relation)


def is_transitive(relation: set) -> bool:
    return all((a, c) in relation for a, b in relation for c, d in relation if b == c)


def is_antisymmetric(relation: set) -> bool:
    for x, y in relation:
        if x == y:
            continue
        if (y, x) in relation:
            return False
    return True


def is_irreflexive(relation: set) -> bool:
    return all((x, x) in relation for x, y in relation)


def strong_anti_symmetric(relation: set) -> bool:
    return is_antisymmetric(relation) and is_irreflexive(relation)


def is_trichotomous(relation, set):
    for x in set:
        for y in set:
            if x != y and ((x, y) in relation or (y, x) in relation):
                continue
            else:
                return False
    return True


def is_dichotomous(set, relation):
    for x in set:
        for y in set:
            if x == y:
                continue
            if not ((x, y) in relation or not (y, x) in relation):
                return False
    return True


def range_of_relation(relation: set):
    values = set()
    for pair in relation:
        values.add(pair[1])
    return values


def equivalence_relation(relation: set) -> tuple[bool, None] | tuple[bool, list[set]]:
    if not is_relexive(relation) or not is_symmetric(relation) or not is_transitive(relation):
        return False, None

    equivalence_classes = []
    remaining_elements = set([a for a, b in relation])
    while remaining_elements:
        a = remaining_elements.pop()
        equivalence_class = {a}
        for b in list(remaining_elements):
            if (a, b) in relation or (b, a) in relation:
                equivalence_class.add(b)
                remaining_elements.remove(b)
        equivalence_classes.append(equivalence_class)

    return True, equivalence_classes
