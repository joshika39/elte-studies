import Data.Char (toUpper)
import Data.List (minimumBy)

monogramm :: String -> String
monogramm str = concatMap (take 1 . map toUpper) (words str)


pointedMonogramm :: String -> String
pointedMonogramm str = concatMap ((: ".") . toUpper . head) (words str)


minByLength :: [[a]] -> [a]
minByLength = minimumBy (\a b -> compare (length a) (length b))