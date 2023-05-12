data Data a = Data Int (Int,Int) | Empty (Int,Int) deriving (Show, Eq)
type DataBase a = [Data a]

getPos :: Data a -> (Int, Int)
getPos dat = case dat of
    Empty pos -> pos
    Data _ pos -> pos

getValue :: Data a -> Int
getValue dat = case dat of
    Empty pos -> 0
    Data d pos -> d

getData :: (Int, Int) -> DataBase a -> Data a
getData pos [] = Empty pos
getData pos (d:ds)
  | getPos d == pos = d
  | otherwise = getData pos ds


sumOfData :: DataBase Int -> Int
sumOfData = foldr ((+) . getValue) 0
