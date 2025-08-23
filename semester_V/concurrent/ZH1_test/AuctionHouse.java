import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ThreadLocalRandom;
import java.util.concurrent.TimeUnit;
import java.util.stream.IntStream;

public class AuctionHouse {
    static class NFT {
        public final int artistIdx;
        public final int price;

        public NFT(int artistIdx, int price) {
            this.artistIdx = artistIdx;
            this.price = price;
        }
    }

    static class AuctionOffer {
        public int offeredSum;
        public String collectorName;

        public AuctionOffer(int offeredSum, String collectorName) {
            this.offeredSum = offeredSum;
            this.collectorName = collectorName;
        }
    }

    static int failCount = 0;

    static final int MAX_NFT_PRICE = 100;
    static final int MAX_NFT_IDX = 100_000;
    static final int MAX_COLLECTOR_OFFER = MAX_NFT_IDX / 100;

    private static final int COLLECTOR_MIN_SLEEP = 10;
    private static final int COLLECTOR_MAX_SLEEP = 20;
    private static final int MAX_AUCTION_OFFERS = 10;

    static final int ARTIST_COUNT = 10;
    static final int COLLECTOR_COUNT = 5;

    static final int INIT_ASSETS = MAX_NFT_IDX / 10 * MAX_NFT_PRICE;

    static int nftIdx = 0;
    static int remainingNftPrice = INIT_ASSETS;
    static NFT[] nfts = new NFT[MAX_NFT_IDX];

    static int totalCommission = 0;
    static int noAuctionAvailableCount = 0;
    static int soldItemCount = 0;

    static BlockingQueue<AuctionOffer> auctionQueue = null;
    static List<String> owners = new ArrayList<>();

    public static void main(String[] args) throws InterruptedException {
        // Task 1: Artists
        List<Thread> artists = makeArtists();

        // Start all artist threads
        for (Thread artist : artists) {
            artist.start();
        }

        // Task 2: Auctioneer
        Thread auctioneer = makeAuctioneer(artists);

        auctioneer.start();

        // Task 3: Collectors
        List<Thread> collectors = makeCollectors(auctioneer);

        for (Thread collector : collectors) {
            collector.start();
        }

        // Wait for all threads to finish
        for (Thread artist : artists) {
            artist.join();
        }
        auctioneer.join();
        for (Thread collector : collectors) {
            collector.join();
        }

        runChecks();
    }
    // ------------------------------------------------------------------------
    // Task 1: Artists
    private static List<Thread> makeArtists() {
        List<Thread> artists = new ArrayList<>();
        for (int i = 0; i < ARTIST_COUNT; i++) {
            final int artistIdx = i;
            Runnable artistTask = () -> {
                while (true) {
                    int price = getRandomBetween(100, 1000);
                    synchronized (nfts) {
                        if (nftIdx >= MAX_NFT_IDX || price > remainingNftPrice) {
                            break;
                        }
                        nfts[nftIdx] = new NFT(artistIdx, price);
                        nftIdx++;
                        remainingNftPrice -= price;
                    }
                    sleepForMsec(20);
                }
            };
            artists.add(new Thread(artistTask));
        }
        return artists;
    }

    // ------------------------------------------------------------------------
    // Task 2: Auctioneer
    private static Thread makeAuctioneer(List<Thread> artists) {
        return new Thread(() -> {
            int finishedArtists = 0;
            while (finishedArtists < ARTIST_COUNT || soldItemCount < 100) {
                synchronized (artists) {
                    finishedArtists = (int) artists.stream().filter(thread -> !thread.isAlive()).count();
                } {
                    synchronized (nfts) {
                        if (nftIdx == 0 || finishedArtists >= ARTIST_COUNT) {
                            sleepForMsec(3);
                            continue;
                        }
                    }
                    int randomIdx = ThreadLocalRandom.current().nextInt(nftIdx);
                    NFT selectedNft = nfts[randomIdx];

                    auctionQueue = new ArrayBlockingQueue<>(MAX_AUCTION_OFFERS);
                    AuctionOffer bestOffer = null;

                    for (int i = 0; i < MAX_AUCTION_OFFERS; i++) {
                        try {
                            AuctionOffer offer = auctionQueue.poll(1, TimeUnit.MILLISECONDS);
                            if (offer != null && (bestOffer == null || offer.offeredSum > bestOffer.offeredSum)) {
                                bestOffer = offer;
                            }
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }

                    if (bestOffer != null) {
                        totalCommission += bestOffer.offeredSum / 10;
                        soldItemCount++;
                        owners.add(bestOffer.collectorName);
                    }

                    auctionQueue = null;
                    sleepForMsec(3);

                    finishedArtists = (int) artists.stream().filter(thread -> !thread.isAlive()).count();
                }
            }
        });
    }

    // ------------------------------------------------------------------------
    // Task 3: Collectors
    private static List<Thread> makeCollectors(Thread auctioneer) {
        List<Thread> collectors = new ArrayList<>();
        for (int i = 0; i < COLLECTOR_COUNT; i++) {
            final String collectorName = "Collector" + (i + 1);
            Runnable collectorTask = () -> {
                while (auctioneer.isAlive()) {
                    sleepForMsec(getRandomBetween(COLLECTOR_MIN_SLEEP, COLLECTOR_MAX_SLEEP));
                    BlockingQueue<AuctionOffer> currentQueue = auctionQueue;
                    if (currentQueue == null) {
                        noAuctionAvailableCount++;
                        continue;
                    }
                    int bid = getRandomBetween(1, MAX_COLLECTOR_OFFER);
                    try {
                        currentQueue.put(new AuctionOffer(bid, collectorName));
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
            };
            collectors.add(new Thread(collectorTask));
        }
        return collectors;
    }

    // ------------------------------------------------------------------------
    // Tester

    private static String isOK(boolean condition) {
        if (!condition)   ++failCount;
        return isOkTxt(condition);
    }

    private static String isOkTxt(boolean condition) {
        return condition ? "GOOD" : "BAD ";
    }

    private static void runChecks() {
        if (Thread.activeCount() == 1) {
            System.out.printf("%s Only the current thread is running%n", isOK(true));
        } else {
            // Print out the active thread count
            System.out.println("Active threads: " + Thread.activeCount());
            System.out.printf("%s %d threads are active, there should be only one%n", isOK(Thread.activeCount() == 1), Thread.activeCount());
        }

        System.out.printf("%s nftIdx > 0%n", isOK(nftIdx > 0));

        int soldPrice = IntStream.range(0, nftIdx).map(idx-> nfts[idx].price).sum();
        System.out.printf("%s Money is not lost: %d + %d = %d%n", isOK(soldPrice + remainingNftPrice == INIT_ASSETS), soldPrice, remainingNftPrice, INIT_ASSETS);

        System.out.printf("%s [Only Task 2] Total commission not zero: %d > 0%n", isOK(totalCommission > 0), totalCommission, INIT_ASSETS);

        System.out.printf("%s [Only Task 3] Sold item count not zero: %d > 0%n", isOK(soldItemCount > 0), soldItemCount, INIT_ASSETS);
        System.out.printf("%s [Only Task 3] Some collectors have become owners of NFTs: %d > 0%n", isOK(owners.size() > 0), owners.size(), INIT_ASSETS);
        System.out.printf("%s [Only Task 3] Sometimes, collectors found no auction: %d > 0%n", isOK(noAuctionAvailableCount > 0), noAuctionAvailableCount, INIT_ASSETS);

        System.out.printf("%s Altogether %d condition%s failed%n", isOkTxt(failCount == 0), failCount, failCount == 1 ? "" : "s");

        // forcibly shutting down the program (don't YOU ever do this)
        System.exit(42);
    }

    // ------------------------------------------------------------------------
    // Utilities

    private static int getRandomBetween(int min, int max) {
        return ThreadLocalRandom.current().nextInt(min, max+1);
    }

    private static void sleepForMsec(int msec) {
        try {
            Thread.sleep(msec);
        } catch (InterruptedException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }
}
