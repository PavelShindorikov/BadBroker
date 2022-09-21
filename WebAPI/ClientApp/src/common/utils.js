const CHART_COLORS = {
    blue: 'rgb(54, 162, 235)',
    yellow: 'rgb(255, 205, 86)',
    green: 'rgb(75, 192, 192)',
    purple: 'rgb(153, 102, 255)',
};
const NAMED_COLORS = [
    CHART_COLORS.blue,
    CHART_COLORS.yellow,
    CHART_COLORS.green,
    CHART_COLORS.purple,
];
export const namedColor = (index) => {
    return NAMED_COLORS[index % NAMED_COLORS.length];
};
