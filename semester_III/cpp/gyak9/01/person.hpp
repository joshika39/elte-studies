//
// Created by JoshH on 11/13/2023.
//

#ifndef CPP_PERSON_H
#define CPP_PERSON_H

class Person {
    std::string name;
    unsigned int age;
public:
    Person(std::string n, unsigned int a): name(n), age(a) { std::cout << name + " letrejott." << std::endl; }
    Person(const Person &p): name(p.name), age(p.age) {}
    ~Person() { std::cout << name + " felszabadult." << std::endl; }
}

#endif //CPP_PERSON_H
