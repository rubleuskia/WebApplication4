const baseApiRoute = `https://localhost:5001/api/`;

export interface RequestOptions {
    body?: unknown;
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
        response = await fetch(baseApiRoute + url, {
            body: getBody(options),
            headers: {
                Accept: "application/json",
                ...getContentTypeHeader(options),
            },
            method,
        });
    } catch (error) {
        console.error(error);
        throw error;
    }

    return handleResponse<T>(response);
};

const getBody = (options?: RequestOptions) => {
    if (!options) {
        return undefined;
    }
    return JSON.stringify(options.body);
};

const getContentTypeHeader = (options?: RequestOptions) => {
    if (options === null || options === undefined) {
        return { "Content-Type": "text/plain" };
    }
    return {
        "Content-Type": typeof options.body === "object" ? "application/json" : "text/plain",
    };
};

const handleResponse = async <T>(response: Response): Promise<T> => {
    if (!response.ok) {
        processFailedResponse(response);
    }

    const text = await response.text();
    return text ? JSON.parse(text) : {};
};

const processFailedResponse = (response: Response) => {
    console.error("Failed response.")
};