import { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';

const SelectedUser = (props) => {

    const { parentUsers } = props;
    const [selectedUser, setSelectedUser] = useState(null);


    return (
        <div>
            <h3>Customer Health:</h3>
            <p> {parentUsers}</p>
        </div>
        //stats

    )

}

export default SelectedUser;
