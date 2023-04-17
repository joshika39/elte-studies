from relations import *
TRAN = "Tranzitív"
SYMM = "Szimmmetrikus"
ANTISYM = "Antiszimmetrikus"
IRREF = "Irreflexív"
SANTISYM = "Szigorúan antiszimmetrikus"
REF = "Reflexív"

TRI = "trichotom"
DI = "dichotom"

def task_one(relation):
    print("1. Feladat:")
    properties = {
        REF: is_relexive(relation),
        TRAN: is_transitive(relation),
        SYMM: is_symmetric(relation),
        ANTISYM: is_antisymmetric(relation),
        IRREF: is_irreflexive(relation)
    }
    properties_str = ""
    for property in properties:
        if properties[property]:
            properties_str += f"{property}, "
    properties_str = properties_str[:-2]
    print(f"Reláció értékkészlete: {range_of_relation(relation)}")
    print(f"Reláció tulajdonságai: {properties_str}")
    print("-----------------------\n---------\n\n")

def task_two(relation):
    print("2. Feladat:")
    is_equivalence_relation, class_resolution = equivalence_relation(relation)
    if is_equivalence_relation:
        print(f"Az osztályfelbontás: {class_resolution}")
    else:
        print(f"A {relation} reláció az nem ekvivalenciareláció.")
    print("-----------------------\n---------\n\n")


if __name__ == '__main__':
    task_one({(1,1), (1,2), (1,3), (2,1), (2,2), (2,3), (3,1), (3,2), (3,3)})
    task_two({(1,1), (1,2), (2,1), (2,2), (3,3)})