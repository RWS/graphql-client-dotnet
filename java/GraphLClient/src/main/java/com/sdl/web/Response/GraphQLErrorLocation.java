package com.sdl.web.Response;

public class GraphQLErrorLocation {
    private int Line;
    private int Column;

    public void setLine(int line) {
        Line = line;
    }

    public int getLine() {
        return Line;
    }

    public void setColumn(int column) {
        Column = column;
    }

    public int getColumn() {
        return Column;
    }
}
