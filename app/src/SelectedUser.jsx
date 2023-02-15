import { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';

const SelectedUser = (props) => {

    const { parentUsers } = props;
    const [selectedUser, setSelectedUser] = useState(null);

    return (
        <div>
            <h3>Customer Health:</h3>
            <p> {parentUsers}</p>
            <table style={{ borderCollapse: 'collapse', width: '100%', marginBottom: '20px' }}>
                <thead>
                    <tr>
                        <th style={{ backgroundColor: "#B6D770", color: '#555', fontWeight: 'bold', textTransform: 'uppercase', padding: '8px', textAlign: 'left' }}>{parentUsers}</th>
                        <th style={{ backgroundColor: "#B6D770", color: '#555', fontWeight: 'bold', textTransform: 'uppercase', padding: '8px', textAlign: 'left' }}>{parentUsers}</th>
                        <th style={{ backgroundColor: "#B6D770", color: '#555', fontWeight: 'bold', textTransform: 'uppercase', padding: '8px', textAlign: 'left' }}>{parentUsers}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style={{ padding: '8px', textAlign: 'left', borderBottom: '1px solid #ddd' }}>{parentUsers}</td>
                        <td style={{ padding: '8px', textAlign: 'left', borderBottom: '1px solid #ddd' }}>{parentUsers}</td>
                        <td style={{ padding: '8px', textAlign: 'left', borderBottom: '1px solid #ddd' }}>{parentUsers}</td>
                    </tr>
                    <tr>
                        <td style={{ padding: '8px', textAlign: 'left', borderBottom: '1px solid #ddd', backgroundColor: '#f2f2f2' }}>{parentUsers}</td>
                        <td style={{ padding: '8px', textAlign: 'left', borderBottom: '1px solid #ddd', backgroundColor: '#f2f2f2' }}>{parentUsers}</td>
                        <td style={{ padding: '8px', textAlign: 'left', borderBottom: '1px solid #ddd', backgroundColor: '#f2f2f2' }}>{parentUsers}</td>
                    </tr>
                </tbody>
            </table>

        </div>
    );
}

export default SelectedUser;
