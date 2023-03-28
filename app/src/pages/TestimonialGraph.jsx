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

const TestimonialGraph = ({ testimonial }) => {

    const weekData = Array.from({ length: 52 }, (_, i) => testimonial[i]
        ? { RequestsSent: testimonial[i].RequestsSent || 0, RequestsCompleted: testimonial[i].RequestsCompleted || 0 }
        : { RequestsSent: 0, RequestsCompleted: 0 }
    );

    const week = Array.from({ length: 52 }, (_, i) => testimonial[i]
        ? { Week: testimonial[i].Week }
        : { Week: i + 1 }
    );
  //  const week = Array.from({ length: 52 }, (_, i) => testimonial[i] ? { Week: testimonial[i].Week } : { Week: 0 });
    console.log("HSKADJDK", week);
    console.log("HELLOOOO",weekData);
    const labels = week.map((week, index) => `Week ${week.Week}`);

    console.log("YAAAA", testimonial);
    const data = {
        labels,
        datasets: [
            {
                label: 'Requests Sent',
                data: weekData.map((week) => week.RequestsSent),
                backgroundColor: 'aqua',
                borderColor: 'black',
                borderWidth: 1,
            },
                {
                label: 'Requests Completed',
                data: weekData.map((week) => week.RequestsCompleted),
                backgroundColor: 'blue',
                borderColor: 'black',
                borderWidth: 1,
            }
        ]
    }

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: true
        },
        scales: {
            y: {
                suggestedMin: 0,
                suggestedMax: 52
                }
        },
        backgroundColor: 'white'

    }

    return (
        <div>
            <Line
                data={data}
            options = {options}

            >
            </Line>
            </div>
      )
}

export default TestimonialGraph