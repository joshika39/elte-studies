import Data.Maybe
data Time = Time Int Int deriving (Show, Eq)
data Job a = Job Time (a -> a)

validateTime :: Time -> Bool
validateTime (Time a b) = hour && min
    where 
        hour = a >= 0 && a <= 23
        min = b >= 0 && b <= 59

isSoonerOrNow (Time t1a t1b) (Time t2a t2b)
    | t1a == t2a && t2b < t1b = True
    | t1a == t2a && t2b > t1b = False
    | t1a >= t2a && t2b <= t1b = True
    | t2a <= t1a = True
    | otherwise = False

isJobDue :: Time -> Job a -> Bool
isJobDue time1 (Job time2 _) = isSoonerOrNow time1 time2 

validateJobElem (Job time _) = not(validateTime time)
extractJobTime (Job time _) = time

validateJobs :: [Job a] -> Maybe Time
validateJobs a
    | length wrongElements > 0 = Just time
    | otherwise = Nothing
    where
        wrongElements = filter ( validateJobElem ) a
        time = extractJobTime (wrongElements !! 0)

performJob :: a -> Job a -> a
performJob a (Job _ f) = f a

performDueJobs :: Time -> a -> [Job a] -> a
performDueJobs time a jobs = foldl performJob a $ filter (isJobDue time) jobs
        
-- performDueJobs (Time 12 30) 0 [ Job (Time 11 0) succ, Job (Time 12 15) succ] == 2
-- performDueJobs (Time 12 30) 0 [ Job (Time 11 0) succ, Job (Time 14 0) succ] == 1
-- performDueJobs (Time 12 30) "" [ Job (Time 11 0) (\xs -> 'a':xs) ] == "a"
-- (performDueJobs (Time 12 30) "" [ Job (Time 11 0) ('b':), Job (Time 12 15) ('a':)]) == "ab"
-- performDueJobs (Time 12 30) "" [ Job (Time 11 0) ('b':), Job (Time 14 0) ('a':)] == "b"
-- performDueJobs (Time 12 30) "" [ Job (Time 12 45) ('b':), Job (Time 14 0) ('a':)] == ""
-- performDueJobs (Time 12 30) "x" [ ] == "x"
-- performDueJobs (Time 23 59) "" (replicate 25 (Job (Time 12 00) ('x':))) == replicate 25 'x'

scheduler :: Time -> b -> [Job b]  -> Either String b
scheduler time a jobs
    | not (validateTime time) = Left ("invalid current time: " ++ show time)
    | isJust (validateJobs jobs) = Left ("invalid job time: " ++ show (fromJust (validateJobs jobs)))
    | otherwise = Right (performDueJobs time a jobs)
    
-- Tests
-- scheduler (Time 12 30) "" [ Job (Time 11 0) ('a':)] == Right "a"
-- (scheduler (Time 12 30) "" [ Job (Time 11 0) ('b':), Job (Time 12 15) ('a':)]) == Right "ab"
-- scheduler (Time 24 60 ) "" [] == Left ("invalid current time: Time 24 60")
-- scheduler (Time 0 0) "" [ Job (Time (-1) (-1)) id ] == Left ("invalid job time: Time (-1) (-1)")
-- scheduler (Time 0 0) "" [ Job (Time 12 30)  id , Job (Time (-1) (-1)) id ] == Left ("invalid job time: Time (-1) (-1)")