let hubConnection;

$(document).ready(function () {
    $('ul.tabs__caption').on('click', 'li:not(.active)', function () {
        $(this)
            .addClass('active').siblings().removeClass('active')
            .closest('div.tabs').next('div.row').find('div.tabs__content').removeClass('active').eq($(this).index()).addClass('active');
        $(this).siblings().children('a').removeClass('active');
        $(this).children('a').addClass('active');
    });

    hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53661/chat")
        .withAutomaticReconnect()
        .build();
    hubConnection.on("SendQuestion", PrintNewQuestion);
    hubConnection.on("SendFeedback", PrintNewFeedback);

    hubConnection.start();

})

const QuestionTemplate = ({ name, text, date }) => `
<div class="media message">
    <img src="/content/person.png" class="mr-3" alt="...">
    <div class="media-body text-break text-justify">
        <h5 class="mt-0">${name}</h5>
        ${text}<br />
        <span class="message-date float-right">${date}</span>
    </div>
</div>
    `;

const FeedbackTemplate = ({ name, text, proposal, date }) => `
<div class="media message">
    <img src="/content/person.png" class="mr-3" alt="...">
        <div class="media-body text-justify text-break">
            <h5 class="mt-0">${name}</h5>
            ${text}<br />
            <h6 class="mt-1">Future Proposal:</h6>
            ${proposal}<br />
            <span class="message-date float-right">${date}</span>
        </div>
</div>
    `;
function PrintNewQuestion(question) {
    let askedDate = new Date(question.asked);

    $('#presentation-' + question.presentationId).prepend([
        {
            name: question.authorName,
            text: question.questionText,
            date: askedDate.getHours() + ':' + askedDate.getMinutes() + ' ' + askedDate.getFullYear() + '/' + askedDate.getMonth() + '/' + askedDate.getDate()
        }
    ].map(QuestionTemplate).join(''));
}

function PrintNewFeedback(feedback) {
    let sentDate = new Date(feedback.sent);

    $('#feedbacks-container').prepend([
        {
            name: feedback.authorName,
            text: feedback.message,
            proposal: feedback.futureProposal,
            date: sentDate.getHours() + ':' + sentDate.getMinutes() + ' ' + sentDate.getFullYear() + '/' + sentDate.getMonth() + '/' + sentDate.getDate()
        }
    ].map(FeedbackTemplate).join(''));
}