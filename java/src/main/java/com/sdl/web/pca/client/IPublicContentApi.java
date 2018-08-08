package com.sdl.web.pca.client;

import com.sdl.web.pca.client.contentmodel.IPagination;
import com.sdl.web.pca.client.contentmodel.InputItemFilter;

import java.io.IOException;

public interface IPublicContentApi {

    <T> T ExecuteItemQuery(InputItemFilter filter, IPagination pagination) throws IOException;
}
