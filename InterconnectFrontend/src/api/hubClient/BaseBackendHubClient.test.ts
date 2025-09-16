import { beforeEach, describe, expect, test, vi } from "vitest";
import BaseBackendHubClient from "./BaseBackendHubClient.ts";

const {
    mockConnect,
    mockWithUrl,
    mockWithAutomaticReconnect,
    mockBuild,
    MockHubConnectionBuilder,
} = vi.hoisted(() => {
    const mockConnect = vi.fn();
    const mockWithUrl = vi.fn();
    const mockWithAutomaticReconnect = vi.fn();
    const mockBuild = vi.fn(() => ({
        start: mockConnect,
    }));

    class MockHubConnectionBuilder {
        public withUrl = mockWithUrl.mockReturnThis();
        public withAutomaticReconnect = mockWithUrl.mockReturnThis();
        public build = mockBuild;
    }

    return {
        mockConnect,
        mockWithUrl,
        mockWithAutomaticReconnect,
        mockBuild,
        MockHubConnectionBuilder,
    };
});

class TestBaseBackendHubClient extends BaseBackendHubClient {
    public async testWithParams() {
        return await this.sendHubRequest("test", "abc");
    }

    public async testWithoutParams() {
        return await this.sendHubRequest("test");
    }

    protected getHubName(): string {
        return "TestHub";
    }
}

vi.mock("@microsoft/signalr", () => ({
    HubConnectionBuilder: MockHubConnectionBuilder,
}));

vi.mock("../../configuration.ts", () => ({
    getConfiguration: () => ({ backendUrl: "http://test/" }),
}));

describe("BaseBackendHubClient", () => {
    beforeEach(() => {
        mockConnect.mockReset();
        mockWithUrl.mockReset();
        mockWithAutomaticReconnect.mockReset();
        mockBuild.mockReset();
    });

    test("should connect to hub when connect is invoked", async () => {
        const testHub = new TestBaseBackendHubClient();

        await testHub.connect();

        expect(mockBuild).toHaveBeenCalledOnce();
        expect(mockConnect).toHaveBeenCalledOnce();
    });

    test("should not attempt to reconnect to hub if sending a request over an active connection", async () => {
        mockBuild.mockImplementation(() => ({
            start: mockConnect,
            invoke: vi.fn(),
            connectionId: "123",
        }));
        const testHub = new TestBaseBackendHubClient();

        await testHub.connect();
        await testHub.testWithoutParams();

        expect(mockConnect).toHaveBeenCalledOnce();
    });

    test("should attempt to reconnect to hub if sending a request over an inactive connection", async () => {
        mockBuild.mockImplementation(() => ({
            start: mockConnect,
            connectionId: null,
        }));
        const testHub = new TestBaseBackendHubClient();

        await testHub.connect();

        expect(mockBuild).toHaveBeenCalledWith();
    });

    test("should connect to correct hub url", async () => {
        const testHub = new TestBaseBackendHubClient();

        await testHub.connect();

        expect(mockWithUrl).toHaveBeenCalledWith("http://test/TestHub");
    });

    test("should send request with params if params are provided", async () => {
        const mockInvoke = vi.fn();
        mockBuild.mockImplementation(() => ({
            start: mockConnect,
            invoke: mockInvoke,
        }));
        const testHub = new TestBaseBackendHubClient();

        await testHub.testWithParams();

        expect(mockInvoke).toHaveBeenCalledWith("test", "abc");
    });

    test("should send request without params if params are not provided", async () => {
        const mockInvoke = vi.fn();
        mockBuild.mockImplementation(() => ({
            start: mockConnect,
            invoke: mockInvoke,
        }));
        const testHub = new TestBaseBackendHubClient();

        await testHub.testWithoutParams();

        expect(mockInvoke).toHaveBeenCalledWith("test");
    });

    test("should retrieve response from hub", async () => {
        const mockInvoke = vi.fn(() => ({ testKey: "testValue" }));
        mockBuild.mockImplementation(() => ({
            start: mockConnect,
            invoke: mockInvoke,
        }));
        const testHub = new TestBaseBackendHubClient();

        const response = await testHub.testWithParams();

        expect(response).toStrictEqual({ testKey: "testValue" });
    });
});
