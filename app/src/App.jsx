import {useEffect, useState} from 'react'
import reactLogo from './assets/react.svg'
import './App.css'

function App() {
    const [parentUsers, setParentUsers] = useState(null);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setLoading(true);
        fetch('https://localhost:7107/api/v1/parentusers')
            .then(response => response.json())
            .then((json) => setParentUsers(json))
            .catch(error => setError(error))
            .finally(() => setLoading(false));
    }, [])

    const renderContent = () => {
        if (loading) return <div>loading...</div>

        if (error) return <div>{error.message}</div>

        return (
            <div style={{width: '100%', textAlign: "left"}}>
                <code>
                <pre>
                    {JSON.stringify(parentUsers, undefined, 2)}
                </pre>
                </code>
            </div>

        )
    }

    return (
        <div className="App">
            {renderContent()}
        </div>
    )
}

export default App
