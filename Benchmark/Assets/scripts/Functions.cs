public class Functions {
    public static int sqrt(int num) {
        if (0 == num) { return 0; } // Avoid zero divide  
        int n = (int) (num * 0.5) + 1; // Initial estimate, never low  
        int n1 = (int) ((n + (num / n)) * 0.5);
        while (n1 < n) {
            n = n1;
            n1 = (int) ((n + (num / n)) * 0.5);
        }
        return n;
    }
}