longerThan :: [a] -> Int -> Bool
longerThan [] 0 = False
longerThan a 0 = True
longerThan [] a = True
longerThan (x:xs) a = longerThan xs (a - 1)

getindex'' :: [Integer] -> [(Integer, Integer)]
getindex'' [] = []
getindex'' a = zip a [0..]

helper :: [(Integer,Integer)] -> [Integer]
helper [] = []
helper ((a,b):xs)
    | odd (a + b) == True = a : helper xs
    | otherwise = helper xs

oddSumSamples :: [Integer] -> [Integer]
oddSumSamples a = helper (getindex'' a)

populationBelow :: Double -> [(String, Double)]  -> [String]
populationBelow test [] = []
populationBelow test ((x,y):xs) 
  | y < test = x : populationBelow test xs
  | otherwise = populationBelow test xs

eqLenPairs :: [String] -> [String] -> [(String, String)]
eqLenPairs [] [] = []
eqLenPairs (x:xs) (y:ys)
  | (length x) == (length y) = (x, y) : eqLenPairs xs ys
  | otherwise = eqLenPairs xs ys


findPlace :: Ord a => a -> [a] -> Integer
findPlace a [] = 0
findPlace a (x:xs)
  | a == x = 1 + findPlace a []
  | a <= x = findPlace a []
  | otherwise = (findPlace a xs) + 1

interval :: Int -> Int -> [Int]
interval 10 0 = []
interval 0 0 = []
interval a b 
  | a < b = a : interval (a + 1) b
  | a > b = []
  | a == b = a : interval 0 0
