import { useState } from "react";
import { Button } from "@mantine/core";

const Counter = () => {
    const [count, setCount] = useState(0);

    return <div>
        <p>Current count: {count}</p>
        <Button onClick={() => setCount(count + 1)}>Click</Button>
    </div>;
};

export default Counter;