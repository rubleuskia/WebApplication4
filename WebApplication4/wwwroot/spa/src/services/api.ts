declare const __API_BASE_URL__: string;

const baseApiRoute = `https://localhost:5001/api/`;

export interface RequestOptions {
    body?: unknown;
    params?: unknown;
}

export const httpGet = async <T extends unknown>(url: string, options?: RequestOptions) => {
    return await request<T>(url, "GET", options);
};

const request = async <T extends unknown>(
    url: string,
    method: "GET" | "POST" | "DELETE" | "PUT",
    options?: RequestOptions,
) => {
    let response: Response;
    try {
        response = await fetch(baseApiRoute + url, { method });
    } catch (error) {
        console.error(error);
        throw error;
    }

    return handleResponse<T>(response);
};

const handleResponse = async <T>(response: Response): Promise<T> => {
    if (!response.ok) {
        throw new Error("Response failure.");
    }

    const text = await response.text();
    return text ? JSON.parse(text) : {};
};
