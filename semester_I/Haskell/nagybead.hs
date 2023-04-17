import Data.Maybe

type Coordinate = (Int, Int)

type Sun = Int

data Plant = 
    Peashooter {plantHP :: Int} | 
    Sunflower {plantHP :: Int} | 
    Walnut {plantHP :: Int} | 
    CherryBomb {plantHP :: Int} deriving (Eq, Show)

data Zombie = 
    Basic {zombieHP :: Int, speed :: Int} | 
    Conehead {zombieHP :: Int, speed :: Int} | 
    Buckethead {zombieHP :: Int, speed :: Int} | 
    Vaulting {zombieHP :: Int, speed :: Int} deriving (Eq, Show)

data GameModel = GameModel Sun [(Coordinate, Plant)] [(Coordinate, Zombie)] deriving (Eq, Show)

defaultPeashooter :: Plant
defaultPeashooter = Peashooter 3

defaultSunflower :: Plant
defaultSunflower = Sunflower 2

defaultWalnut :: Plant
defaultWalnut = Walnut 15

defaultCherryBomb :: Plant
defaultCherryBomb = CherryBomb 2

basic :: Zombie
basic = Basic 5 1

coneHead :: Zombie
coneHead = Conehead 10 1

bucketHead :: Zombie
bucketHead = Buckethead 20 1

vaulting :: Zombie
vaulting = Vaulting 7 2

killZombie :: Zombie -> Zombie
killZombie z = lowerZombieHP z (zombieHP z)

validCoordinate :: Coordinate -> Bool
validCoordinate (y, x) = (elem y  [0 .. 4]) && (elem x [0 .. 11])

-- Task 1

plantName :: Plant -> String
plantName plant = head (words (show plant))

plantDefaultPrices :: [(String, Int)]
plantDefaultPrices = [("Sunflower", 50), ("Walnut", 50), ("Peashooter", 100), ("CherryBomb", 150)]

pPosCheck :: GameModel -> Coordinate  -> Bool
pPosCheck (GameModel _ plants _) coor = not (any (\(plantCoor, _) -> plantCoor == coor) plants)

tryPurchase :: GameModel -> Coordinate -> Plant -> Maybe GameModel
tryPurchase (GameModel sun pantCoor zombieCoor) coor plant
    | not (validCoordinate coor) = Nothing
    | not (pPosCheck (GameModel sun pantCoor zombieCoor) coor) = Nothing
    | price > sun = Nothing
    | otherwise = Just ( GameModel (sun - price) ((coor, plant) : pantCoor) zombieCoor)
    where
        price :: Int
        price = fromJust $ lookup (plantName plant) plantDefaultPrices
-- ----------------------------------------------------------------

-- Task 2

zPosCheck :: GameModel -> Coordinate  -> Bool
zPosCheck (GameModel _ _ zombies) coor = not (any (\(plantCoor, _) -> plantCoor == coor) zombies)

placeZombieInLane :: GameModel -> Zombie -> Int -> Maybe GameModel
placeZombieInLane (GameModel sun plantCoor zombieCoor) z y
    | not (validCoordinate (y, 11)) = Nothing
    | not (zPosCheck (GameModel sun plantCoor zombieCoor) (y, 11)) = Nothing
    | otherwise = Just (GameModel sun plantCoor (((y, 11), z) : zombieCoor))
-- ----------------------------------------------------------------

-- Task 3

lowerZombieHP :: Zombie -> Int -> Zombie
lowerZombieHP zombi x = case zombi of
    Basic hp speed -> Basic (hp - x) speed
    Conehead hp speed -> Conehead (hp - x) speed
    Buckethead hp speed -> Buckethead (hp - x) speed
    Vaulting hp speed -> Vaulting (hp - x) speed

lowerPlantHP :: Plant -> Int -> Plant
lowerPlantHP plant x = case plant of
    Peashooter hp -> Peashooter (hp - x)
    Sunflower hp -> Sunflower (hp - x)
    Walnut hp -> Walnut (hp - x)
    CherryBomb hp -> CherryBomb (hp - x)

performZombieActions :: GameModel -> Maybe GameModel
performZombieActions (GameModel s placedP placedZ)
    | any (\((_, x), _) -> x <= 0) placedZ = Nothing
    | otherwise = Just (GameModel s (map processPlant placedP) (map moveZombie placedZ))
    where 
        moveZombie :: (Coordinate, Zombie) -> (Coordinate, Zombie)
        moveZombie (zombieCoor, Vaulting hp 2)
            | previousPlant = ((fst zombieCoor, snd zombieCoor - 2), Vaulting hp 1)
            | onBoardPlant = ((fst zombieCoor, snd zombieCoor - 1), Vaulting hp 1)
            | otherwise = ((fst zombieCoor, snd zombieCoor - 2), Vaulting hp 2)
            where
                previousPlant = any (\(plantCoor, _) -> plantCoor == (fst zombieCoor, snd zombieCoor - 1)) placedP
                onBoardPlant = any (\(plantCoor, _) -> plantCoor == zombieCoor) placedP
        moveZombie (zombieCoor, z)
            | onBoardPlant = (zombieCoor, z)
            | otherwise = ((fst zombieCoor, snd zombieCoor - speed z), z)
            where
                onBoardPlant = any (\(plantCoor, _) -> plantCoor == zombieCoor) placedP
        
        processPlant :: (Coordinate, Plant) -> (Coordinate, Plant)
        processPlant (coor, plant) = (coor, lowerPlantHP plant (length (filter isOnTile placedZ)))
            where
                isOnTile :: (Coordinate, Zombie) -> Bool
                isOnTile (_, Vaulting _ 2) = False
                isOnTile (zombieCoor, _) = zombieCoor == coor

cleanBoard :: GameModel -> GameModel
cleanBoard (GameModel sun plantCoor zombieCoor) = GameModel sun (filter livingPlant plantCoor) (filter livingZombie zombieCoor)
  where
    livingPlant (coor, plant) = plantHP plant > 0
    livingZombie (coor, zombie) = zombieHP zombie > 0 -- What a contradiction.. a zombie can not be alive but bear with me 😂😂

calculateSun sun plants = sun + sum (map newSun plants)
    where
        newSun (c, Sunflower _) = 25
        newSun _ = 0

operatePlant (coor, CherryBomb _) = (coor, CherryBomb 0)
operatePlant x = x

testBomb plants = filter test plants
    where
        test (_, CherryBomb _) = True
        test _ = False

testPeasshooter plants zombieCoor = filter test plants
    where
        test (coor, Peashooter _) = fst zombieCoor == fst coor && snd coor <= snd zombieCoor
        test _ = False

zombieTest posX zombieCoor zombies =  any (\((_,x), _) -> x `elem` [posX .. snd zombieCoor - 1]) zombies
-- -------------------------------------------------------------------------------- -------------

-- Task 4

performPlantActions :: GameModel -> GameModel
performPlantActions (GameModel s placedP placedZ) = GameModel (calculateSun s placedP) (map operatePlant placedP) (map zombieAction placedZ)
    where
        zombieAction (zombieCoor, z)
            | bombArea = (zombieCoor, killZombie z)
            | otherwise = (zombieCoor, lowerZombieHP z damage)
            where
                damage
                    | null (filter (\((_,x),_) -> not (zombieTest x zombieCoor placedZ)) (testPeasshooter placedP zombieCoor)) = 0
                    | otherwise = length (filter (\((_,x),_) -> not (zombieTest x zombieCoor placedZ)) (testPeasshooter placedP zombieCoor))
                areaSize = map (\((y, x), _) -> ([y - 1 .. y + 1], [x - 1 .. x + 1])) (testBomb placedP)
                bombArea = any (\(ry, rx) -> fst zombieCoor `elem` ry && snd zombieCoor `elem` rx) areaSize