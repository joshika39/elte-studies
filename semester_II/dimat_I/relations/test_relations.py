import unittest
from relations import *

class TestSum(unittest.TestCase):

    def test_0001_range_of_relation(self):
        self.assertEqual(range_of_relation({(1,2), (2,3), (3,4)}), {2, 3, 4}, "Should be {1, 2, 3, 4}")
    
    def test_0005_range_of_relation_with_negative_numbers(self):
        self.assertEqual(range_of_relation({(-2, -5), (3, 2), (4, 7), (4, 8),(9, 11)}), {-5, 2, 7, 8, 11}, "Should be {-5, 2, 7, 8, 11}")

    def test_0011_equivalence_relation(self):
        self.assertEqual(equivalence_relation({(1,1), (1,2), (2,1), (2,2), (3,3)}), (True, [{1, 2}, {3}]), "Should be (True, [{1, 2}, {3}])")

    def test_0021_is_antisymmetric(self):
        self.assertEqual(is_antisymmetric({(1, 2), (2, 3), (3, 4)}), True, "Should be: True")
        self.assertEqual(is_antisymmetric({(1, 2), (2, 1), (3, 3)}), False, "Should be: False")
        self.assertEqual(is_antisymmetric({(1, 1), (2, 2), (3, 3)}), True, "Should be: True")

    def test_0021_is_antisymmetric(self):
        self.assertEqual(is_antisymmetric({(1, 2), (2, 3), (3, 4)}), True, "Should be: True")
        self.assertEqual(is_antisymmetric({(1, 2), (2, 1), (3, 3)}), False, "Should be: False")
        self.assertEqual(is_antisymmetric({(1, 1), (2, 2), (3, 3)}), True, "Should be: True")
if __name__ == '__main__':
    unittest.main()