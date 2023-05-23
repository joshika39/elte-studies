-- Színek definíciója
data Color = Purple | DarkGreen | Orange | Pink | Yellow | Red | Gray | DarkBlue | Green | Blue deriving (Eq, Show)

-- Tippek és kitalálandó színsor típusai
type Guess = [Color]
type ColorRow = [Color]
type AvailableGuesses = Int

-- Jelölő típusa
data Mark = Black | White deriving (Eq, Show)

-- Visszajelzés típusa
data GuessResp = Resp Guess [Mark] deriving (Eq, Show)

-- Végeredmény típusa
data Completion = Success Int | Failure String | Ongoing deriving (Eq, Show)

-- Játék típusa
data Game = Game ColorRow (AvailableGuesses, [GuessResp]) Completion deriving (Eq, Show)

-- Előre elkészített játékok
emptyGame1 :: Game
emptyGame1 = Game [Red, DarkGreen, Pink, Orange] (10, []) Ongoing

emptyGame2 :: Game
emptyGame2 = Game [Yellow, Gray, Green, DarkBlue] (3, []) Ongoing

wrongGame1 :: Game
wrongGame1 = Game [Red] (10, []) Ongoing

wrongGame2 :: Game
wrongGame2 = Game (repeat Purple) (2, []) Ongoing


checkLength :: Int -> Guess -> ColorRow -> Bool
checkLength _ [] [] = True
checkLength _ [] l = False
checkLength _ l [] = False
checkLength n (l1:l1s) (l2:l2s) = checkLength (n - 1) l1s l2s

-- matchingColorsRightPlace függvény megvalósítása
matchingColorsRightPlace :: Guess -> ColorRow -> Maybe Int

matchingColorsRightPlace guess colors
  | not (checkLength 4 guess colors) = Nothing
  | otherwise = Just $ length $ filter id $ zipWith (==) guess colors