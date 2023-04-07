import Data.Char (chr)
import Data.List (isPrefixOf)

dictionary :: [String]
dictionary = [ [chr x] | x <- [0..127]]

prefixes :: String -> [String] -> [(Int,String)]
prefixes y xs = [(i, xs !! i) | i <- [0 .. length xs - 1], (xs !! i) `isPrefixOf` y]
-- prefixes y xs = [(i, xs !! i) | i <- [0 .. length xs - 1], isPrefixOf (xs !! i) y]

longest :: [(Int,String)] -> (Int,String)
longest xs = helper xs (0,"")
  where
    helper [] acc = acc
    helper (x:xs) acc
      | length (snd acc) < length (snd x) = helper xs x
      | otherwise = helper xs acc


munch :: [String] -> String -> (Int,String,String)
munch dict str = (fst longst, snd longst, [str !! x | x <- [length (snd longst)..length str - 1]])
  where
    longst = longest (prefixes str dict) 

append :: [String] -> String -> String -> [String]
append dict _ "" = dict
append dict w1 w2
  | notElem word dict = dict ++ [word] 
  | otherwise = dict
  where
    word = w1 ++ (take 1 w2)