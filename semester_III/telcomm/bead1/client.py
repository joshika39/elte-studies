import sys
import os
import json


class Link:
    def __init__(self, start, end, capacity):
        self.is_reserved = False
        self.start = start
        self.end = end
        self.capacity = capacity

    def __str__(self):
        return f'{self.start} <-> {self.end}'


class Circuit:
    def __init__(self, _links):
        self.is_locked = False
        self.links = _links
        self.start = _links[0].start
        self.end = _links[-1].end

    def __str__(self):
        result = ""
        for i, link in enumerate(self.links):
            if i == len(self.links) - 1:
                result += f'{link.start} <-> {link.end}'
            else:
                result += f'{link.start} <-> '
        return result

    def is_available(self):
        return all(not link.is_reserved for link in self.links)

    def reserve(self):
        self.is_locked = True
        for link in self.links:
            link.is_reserved = True

    def free(self):
        self.is_locked = False
        for link in self.links:
            link.is_reserved = False


class LeftCircuit(Circuit):
    def __init__(self, _links):
        super().__init__(_links)
        self.start = _links[0].end
        self.end = _links[-1].start
        self.links.reverse()

    def __str__(self):
        result = ""
        for i, link in enumerate(self.links):
            if i == len(self.links) - 1:
                result += f'{link.end} <-> {link.start}'
            else:
                result += f'{link.end} <-> '
        return result


def find_link(start: str, end: str, _links):
    for link in _links:
        if link.start == start and link.end == end:
            return link

        if link.end == start and link.start == end:
            return link


def get_links(json_obj):
    link_list = []
    for link in json_obj["links"]:
        link_list.append(Link(link["points"][0], link["points"][1], link["capacity"]))
    return link_list


def get_circuits(json_obj) -> [Circuit]:
    _links = get_links(json_obj)

    circuit_list = []
    for circuit in json_obj["possible-circuits"]:
        c_links = []
        for i, point in enumerate(circuit):
            if i + 1 < len(circuit):
                link = find_link(circuit[i], circuit[i + 1], links)
                if link is not None:
                    c_links.append(link)
        circuit_list.append(Circuit(c_links))
    return circuit_list


def find_available_circuit(start: str, end: str, _circuits):
    for circuit in _circuits:
        if circuit.start == start and circuit.end == end:
            return circuit

        if circuit.start == end and circuit.end == start:
            return LeftCircuit(circuit.links)


if __name__ == '__main__':
    if len(sys.argv) < 2:
        print("Missing input file")
        exit(1)
    filePath = sys.argv[1]
    print(filePath)
    if not os.path.exists(filePath):
        print(f"Non-existent file: {filePath}")
        exit(1)

    json_data = open(filePath, 'r').read()
    data = json.loads(json_data)

    duration = int(data['simulation']['duration'])
    demands = data['simulation']['demands']
    event = 1
    links = get_links(data)
    circuits = get_circuits(data)

    for i in range(1, duration + 1):
        processed_demand = []
        for demand in demands:
            endpoints = demand["end-points"]
            c = find_available_circuit(endpoints[0], endpoints[1], circuits)
            if c is None:
                continue

            if int(demand["start-time"]) == i and demand not in processed_demand:
                if c.is_available():
                    print(f'{event}. igény foglalás: {c.start}<->{c.end} st:{i} – sikeres')
                    c.reserve()
                else:
                    print(f'{event}. igény foglalás: {c.start}<->{c.end} st:{i} – sikertelen')
                event += 1
                processed_demand.append(demand)
            if int(demand["end-time"]) == i and demand not in processed_demand:
                if c.is_locked:
                    print(f'{event}. igény felszabadítás: {c.start}<->{c.end} st:{i}')
                    c.free()
                    event += 1
                    processed_demand.append(demand)
