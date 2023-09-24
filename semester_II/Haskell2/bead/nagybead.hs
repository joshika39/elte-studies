import Data.List (sort, intersect)
import Data.Char (toUpper)
import Data.Either ()
import Data.Maybe (isNothing, fromJust)
-- Színek definíciója
data Color = Purple | DarkGreen | Orange | Pink | Yellow | Red | Gray | DarkBlue | Green | Blue deriving (Eq, Show)

-- Tippek és kitalálandó színsor típusai
type Guess = [Color]
type ColorRow = [Color]
type AvailableGuesses = Int

-- Jelölő típusa
data Mark = Black | White deriving (Eq, Show, Ord)

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
checkLength 0 (_:xs) (_:ys) = False
checkLength n [] [] = n == 0
checkLength _ [] l = False
checkLength _ l [] = False
checkLength n (l1:l1s) (l2:l2s) = checkLength (n - 1) l1s l2s

matchingColorsRightPlace :: Guess -> ColorRow -> Maybe Int
matchingColorsRightPlace guess colors
  | not (checkLength 4 guess colors) = Nothing
  | otherwise = Just $ length $ filter id $ zipWith (==) guess colors

guessInColor :: Guess -> ColorRow -> Int
guessInColor [] [] = 0
guessInColor (l1:l1s) (l2:l2s)
  | elem l1 (l2:l2s) && l1 /= l2 = 1 + guessInColor l1s l2s
  | otherwise = guessInColor l1s l2s

countCommonElements :: [Color] -> [Color] -> Int
countCommonElements list1 list2 = length (list1 `intersect` list2)

matchingColorsWrongPlace :: Guess -> ColorRow -> Maybe Int
matchingColorsWrongPlace guess colors
  | not (checkLength 4 guess colors) = Nothing
  | otherwise = Just (countCommonElements guess colors - fromJust (matchingColorsRightPlace guess colors))


getMarks :: Guess -> ColorRow -> [Mark]
getMarks guess colors = replicate black Black ++ replicate white White
  where
    black = fromJust $ matchingColorsRightPlace guess colors
    white = fromJust $ matchingColorsWrongPlace guess colors

guessOnce :: Guess -> ColorRow -> AvailableGuesses -> Maybe (AvailableGuesses, GuessResp)
guessOnce _ _ 0 = Nothing
guessOnce guess colors remGuess
  | not $ checkLength 4 guess colors || remGuess == 0 = Nothing
  | otherwise = Just (remGuess - 1, Resp guess (getMarks guess colors))

checkWinningMarks :: [Mark] -> Bool
checkWinningMarks [Black, Black, Black, Black] = True
checkWinningMarks _ = False

gameUpdate :: Maybe Guess -> Either String Game -> Either String Game
gameUpdate _ (Right (Game _ _ (Failure _))) = Left "Ezt a jatekot mar korabban elvesztetted!"
gameUpdate _ (Right (Game _ _ (Success _))) = Left "Ezt a jatekot mar korabban megnyerted!"
gameUpdate Nothing (Right (Game c resp Ongoing)) = Right $ Game c resp (Failure "Feladtad a jatekot!")
gameUpdate guess (Right (Game colors (rem, guessResp) _))
  | not (checkLength 4 (fromJust guess) colors) = Left "A tippeles nem sikerult!"
  | fromJust (matchingColorsRightPlace (fromJust guess) colors) == 4 = Right $ Game colors (newRem, newResp) (Success $ length newResp)
  | fst remGuess == 0 = Right $ Game colors (newRem, newResp) (Failure "Elfogytak a tippek!")
  | otherwise = Right $ Game colors (newRem, newResp) Ongoing
  where
    remGuess = fromJust (guessOnce (fromJust guess) colors rem)
    resp = remGuess
    newRem = fst remGuess
    newResp = guessResp ++ [snd resp]


gameState :: Either String Game -> String
gameState (Left "") = "HIBA"
gameState (Left string) = "HIBA: " ++ string
gameState (Right (Game _ (rem, _) Ongoing)) = "A jatek meg folyamatban van, meg " ++ show rem ++ " tipped van hatra."
gameState (Right (Game _ _ (Success tipps) )) = "A jatekot megnyerted " ++ show tipps ++" tippbol."
gameState (Right (Game _ _ (Failure str) )) = "A jatekot elvesztetted. " ++ str


stringToColor :: String -> Maybe Color
stringToColor colorStr
  | map toUpper colorStr == "PURPLE" = Just Purple
  | map toUpper colorStr == "DARKGREEN" = Just DarkGreen
  | map toUpper colorStr == "ORANGE" = Just Orange
  | map toUpper colorStr == "PINK" = Just Pink
  | map toUpper colorStr == "YELLOW" = Just Yellow
  | map toUpper colorStr == "RED" = Just Red
  | map toUpper colorStr == "GRAY" = Just Gray
  | map toUpper colorStr == "DARKBLUE" = Just DarkBlue
  | map toUpper colorStr == "GREEN" = Just Green
  | map toUpper colorStr == "BLUE" = Just Blue
  | otherwise = Nothing

stringToColor' :: String -> Color
stringToColor' c = fromJust (stringToColor c)

split :: Char -> String -> [String]
split c s = case rest of
  []     -> [chunk]
  _:rest -> chunk : split c rest
  where
    (chunk, rest) = break (==c) s

trim :: String -> String
trim xs = [ x | x <- xs, x `notElem` " \t" ]

checkList :: [String] -> Bool
checkList [] = True
checkList (l:ls)
  | isNothing (stringToColor l) = False
  | otherwise = checkList ls

convertList :: [String] -> [Color]
convertList = map stringToColor'

stringToGuess :: String -> Maybe Guess
stringToGuess string
  | not (checkList strList) = Nothing
  | otherwise = Just (convertList strList)
  where
    strList = split ',' (trim string)

gameUpdateFromString :: String -> Either String Game -> Either String Game
gameUpdateFromString str = gameUpdate (stringToGuess str)
