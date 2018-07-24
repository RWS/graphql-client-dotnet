package com.sdl;

public class GraphQLErrorLocation {
    private int Line;
    private int Column;

    public int getLine() {
        return Line;
    }

    public void setLine(int line) {
        Line = line;
    }

    public int getColumn() {
        return Column;
    }

    public void setColumn(int column) {
        Column = column;
    }
}
