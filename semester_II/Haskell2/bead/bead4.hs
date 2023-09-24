import Data.Char

isLonger :: [a] -> [b] -> Bool

isLonger [] [] = False
isLonger (a:xs) [] = True
isLonger [] (a:xs) = False
isLonger (a:xs) (b:ys) = isLonger xs ys

zipper :: [a] -> [b] -> [(a,b)]

zipper a [] = []
zipper [] b = []
zipper (a:as) (b:bs) = (a,b) : zipper as bs

(+++) :: [a] -> [a] -> [a]
(+++) [] ys = ys
(+++) (x:xs) ys = x : (+++) xs ys

-- Szebb megoldas (csak a foldr-t meg nem tanultuk)
(++++) :: [a] -> [a] -> [a]
(++++) xs ys = foldr (:) ys xs

camelToWords :: String -> String
camelToWords "" = ""
camelToWords (c:cs) 
  | isUpper c = ' ' : c : camelToWords cs
  | otherwise = c : camelToWords cs