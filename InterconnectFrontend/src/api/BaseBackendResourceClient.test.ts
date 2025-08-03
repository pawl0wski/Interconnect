import { describe, expect } from "vitest";
import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import BaseResponse from "./responses/BaseResponse.ts";

type TestResponse = BaseResponse<string>;

class TestBaseResourceClient extends BaseBackendResourceClient {
    public async postTest(): Promise<TestResponse> {
        return this.sendBackendRequest("testMethod", "POST", { test: true });
    }

    public async getTest(): Promise<TestResponse> {
        return this.sendBackendRequest("testMethod", "GET", null);
    }

    protected getResourceName(): string {
        return "Test";
    }
}

describe("BaseBackend", () => {
    vi.mock(
        "../configuration.ts", () => ({
            getConfiguration: () => ({ backendUrl: "http://test/" })
        })
    );

    test("should send request", async () => {
        // @ts-ignore
        global.fetch = vi.fn(() => Promise.resolve({
            json: () => Promise.resolve({
                success: true,
                message: null,
                data: {}
            })
        }));
        const client = new TestBaseResourceClient();

        await client.postTest();

        expect(fetch).toHaveBeenCalledWith("http://test/Test/testMethod", {
            method: "POST",
            body: JSON.stringify({ test: true }),
            headers: {
                "Content-Type": "application/json"
            }
        });
    });

    test("should throw error when backend return error", async () => {
        // @ts-ignore
        global.fetch = vi.fn(() => Promise.resolve({
            json: () => Promise.resolve({
                success: false,
                message: "Mock error",
                data: {}
            })
        }));
        const client = new TestBaseResourceClient();

        await expect(client.postTest()).rejects.toThrow("Mock error");
    });

    test("should not send body when method is GET", async () => {
        // @ts-ignore
        global.fetch = vi.fn(() => Promise.resolve({
            json: () => Promise.resolve({
                success: true,
                message: null,
                data: {}
            })
        }));
        const client = new TestBaseResourceClient();

        await client.getTest();

        expect(fetch).toHaveBeenCalledWith("http://test/Test/testMethod", {
            method: "GET"
        });
    });
});