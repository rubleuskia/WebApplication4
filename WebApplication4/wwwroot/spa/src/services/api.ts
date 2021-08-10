const baseApiRoute = `https://localhost:5001/api/`;

export interface RequestOptions {
    body?: unknown;
    params?: unknown;
}

export const httpGet = async <T extends unknown>(url: string, options?: RequestOptions) => {
    return await request<T>(url, "GET", options);
};

export const httpPost = async <T extends unknown>(url: string, options?: RequestOptions) => {
    return await request<T>(url, "POST", options);
};

const request = async <T extends unknown>(
    url: string,
    method: "GET" | "POST" | "DELETE" | "PUT",
    options?: RequestOptions,
) => {
    let response: Response;
    try {
        const requestHeaders = new Headers();
        requestHeaders.set('Content-Type', getContentTypeHeader(options));
        requestHeaders.set('Accept', 'application/json');

        response = await fetch(baseApiRoute + url, {
            body: getBody(options),
            headers: requestHeaders,
            method,
        });
    } catch (error) {
        console.error(error);
        throw error;
    }

    return handleResponse<T>(response);
};


const getContentTypeHeader = (options?: RequestOptions) => {
    if (options === null || options === undefined) {
        return "text/plain";
    }
    if (options.body instanceof FormData) {
        return "";
    }
    return "application/json";
};

const getBody = (options?: RequestOptions) => {
    if (!options) {
        return undefined;
    }
    return JSON.stringify(options.body);
};

const handleResponse = async <T>(response: Response): Promise<T> => {
    if (!response.ok) {
        throw new Error("Response failure.");
    }

    const text = await response.text();
    return text ? JSON.parse(text) : {};
};
