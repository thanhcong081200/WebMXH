
var postApiUrl = '/api/WallPost/', commentApiUrl = '/api/Comment/';


// Model
function Post(data) {
    var self = this;
    data = data || {};
    self.IDBAIVIET = data.IDBAIVIET;
    self.NOIDUNG = ko.observable(data.NOIDUNG || "");
    self.USERID = data.USERID || "";
    self.USERNAME = data.USERNAME || "";
    self.HINHANH = data.HINHANH || "";
    self.NGAYDANG = getTimeAgo(data.NGAYDANG);
    self.error = ko.observable();
    self.COMMENT = ko.observableArray();

    self.newCommentMessage = ko.observable();
    self.addComment = function () {
        var comment = new Comment();
        comment.IDBAIVIET = self.IDBAIVIET;
        comment.NOIDUNG(self.newCommentMessage());
        return $.ajax({
            url: commentApiUrl,
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: 'POST',
            data: ko.toJSON(comment)
        })
            .done(function (result) {
                self.PostComments.push(new Comment(result));
                self.newCommentMessage('');
            })
            .fail(function () {
                error('unable to add post');
            });


    }
    if (data.COMMENT) {
        var mappedPosts = $.map(data.COMMENT, function (item) { return new Comment(item); });
        self.COMMENT(mappedPosts);
    }
    self.toggleComment = function (item, event) {
        $(event.target).next().find('.publishComment').toggle();
    }
}



function Comment(data) {
    var self = this;
    data = data || {};

    // Persisted properties
    self.IDCMT = data.IDCMT;
    self.IDBAIVIET = data.IDBAIVIET;
    self.NOIDUNG = ko.observable(data.NOIDUNG || "");
    self.USERID = data.USERID || "";
    self.HINHANH = data.HINHANH || "";
    self.USERNAME = data.USERNAME || "";
    self.THOIGIAN = getTimeAgo(data.THOIGIAN);
    self.error = ko.observable();
    //persist edits to real values on accept
    self.deleteComment = function () {

    }

}

function getTimeAgo(varDate) {
    if (varDate) {
        return $.timeago(varDate.toString().slice(-1) == 'Z' ? varDate : varDate + 'Z');
    }
    else {
        return '';
    }
}


function viewModel() {
    var self = this;
    self.BAIVIET = ko.observableArray();
    self.newMessage = ko.observable();
    self.error = ko.observable();
    self.loadPosts = function () {
        //To load existing posts
        $.ajax({
            url: postApiUrl,
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: 'GET'
        })
            .done(function (data) {
                var mappedPosts = $.map(data, function (item) { return new Post(item); });
                self.posts(mappedPosts);
            })
            .fail(function () {
                error('unable to load posts');
            });
    }

    self.addPost = function () {
        var post = new Post();
        post.NOIDUNG(self.newMessage());
        return $.ajax({
            url: postApiUrl,
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: 'POST',
            data: ko.toJSON(post)
        })
            .done(function (result) {
                self.posts.splice(0, 0, new Post(result));
                self.newMessage('');
            })
            .fail(function () {
                error('unable to add post');
            });
    }
    self.loadPosts();
    return self;
};

//custom bindings

//textarea autosize
ko.bindingHandlers.jqAutoresize = {
    init: function (element, valueAccessor, aBA, vm) {
        if (!$(element).hasClass('msgTextArea')) {
            $(element).css('height', '1em');
        }
        $(element).autosize();
    }
};

ko.applyBindings(new viewModel());

