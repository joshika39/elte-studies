public class JavaThreadsQuiz {
  public static void main(String[] args) {
    Thread thread = new Thread() {
      @Override
      public void run() {
        for (int i = 1; i <= 10; i++) {
          System.out.println(i);
        }
      try {
        Thread.sleep(1000);
      }  catch (InterruptedException e) {
       System.out.println("I am interrupted");
      }
   }  
 };
    System.out.println(thread.isAlive());

   thread.start();
   thread.interrupt();
 }
}
