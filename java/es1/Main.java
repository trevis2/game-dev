public class Main {

    public static void main(String[] args) {
        System.out.println("hello");

        Time tempo1 = new Time("1", null);
        Time tempo2 = new Time("2", tempo1);
        System.out.println(tempo2);
    }
}

class Time {
    private String times;
    private Time time2;

    public Time() {

    }

    public Time(String t, Time t2) {
        this.times = t;
        this.time2 = t2;
    }

    void setTest() {
        System.out.println(time2.times);
    }

    @Override
    public String toString() {

        return "times:" + times + "\n" + "Time:" + time2;
    }
}
