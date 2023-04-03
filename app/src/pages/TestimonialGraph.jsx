import React from 'react';
import { Line } from 'react-chartjs-2'
import {
    Chart as ChartJS,
    BarElement,
    CategoryScale,
    LinearScale,
    Tooltip,
    PointElement,
    LineElement,
    LineController,
    Legend
} from 'chart.js';

ChartJS.register(
    BarElement,
    CategoryScale, // x axis weeks
    LinearScale, // y axis number of requests
    Tooltip,
    Legend,
    PointElement,
    LineElement,
    LineController
    )

const TestimonialGraph = ({ testimonial }) => { // testimonial: list of weeks with data and the number

    function getWeek() {
        const currentDate = new Date();
        const startDate = new Date(currentDate.getFullYear(), 0, 1);
        const days = Math.floor((currentDate - startDate) / (24 * 60 * 60 * 1000));
        const weekNumber = Math.ceil(days / 7);
        return weekNumber;
    }

    const currentWeek = getWeek();

    const weekData = Array.from({ length: 52 }, (_, i) => {
        // const index = i + 1;
        if (testimonial[i]) { // if value exists
            return {
                RequestsSent: testimonial[i].RequestsSent || 0,
                RequestsCompleted: testimonial[i].RequestsCompleted || 0,
                Year: testimonial[i].Year || 0,
                Week: testimonial[i].Week || 0
            }; // return those elememts in array
        } else {
            return { RequestsSent: 0, RequestsCompleted: 0, Week: i }; // else return 0
        }
    });

    const requestsCompleted = weekData.map((data) => data.RequestsCompleted);   // num requests completed [15,1,11,3,7,6,0,8,44]        (this data is for Redi_carpet)
    const requestsSent = weekData.map((data) => data.RequestsSent);             // num requests sent?    
    const weeksCompleted = weekData.map((data) => data.Week);                   // corresponding weeks    [31, 33, 37, 41, 45, 50, 1, 5, 9]

 
    const weeks = Array.from({ length: 53 }, (_, i) => (currentWeek + i > 52 ? currentWeek + i - 52 : currentWeek + i));

    const formatted_dictionary = [];
    for (let i = 1; i < weeks.length; i++) {
        const weekData = {
            RequestsSent: 0,
            RequestsCompleted: 0,
            Year: currentWeek - weeks[i] >= 0 ? new Date().getFullYear() : new Date().getFullYear() - 1,
            Week: weeks[i],
        };
        const index = weeksCompleted.indexOf(weekData.Week);
        if (index !== -1) {
            weekData.RequestsCompleted = requestsCompleted[index];
            weekData.RequestsSent = requestsSent[index];
        }
        formatted_dictionary.push(weekData);
    }

    console.log("testimonial data: ", formatted_dictionary);

    const week = formatted_dictionary.map(item => ({ Year: item.Year, Week: item.Week }));
    const labels = week.map((week, index) => `Week ${week.Week}, ${week.Year}`);

    const data = {
        labels,
        datasets: [
            {
                label: 'Requests Sent',
                data: formatted_dictionary.map((week) => week.RequestsSent),
                backgroundColor: '#baf33c',
                borderColor: 'black',
                borderWidth: 1,
            },
                {
                label: 'Requests Completed',
                data: formatted_dictionary.map((week) => week.RequestsCompleted),
                backgroundColor: '#455c11',
                borderColor: 'black',
                borderWidth: 1,
            }
        ]
    }

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: true,
            text: 'My Chart Title'
        },
        scales: {
            y: {
                suggestedMin: 0,
                suggestedMax: testimonial.length
                }
        },

    }

    return (
        <div>
            <Line
                data={data}
                options={options}
                style={{ height: "400px", width: "600px" }} 

            >
            </Line>
            </div>
      )
}


export default TestimonialGraph