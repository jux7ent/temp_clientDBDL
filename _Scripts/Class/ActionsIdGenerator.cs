using DBDL.CommonDLL;

public static class ActionsIdGenerator {
    private static IdGenerator generator = new IdGenerator(300);

    public static int GenId() {
        return generator.GenId();
    }
}