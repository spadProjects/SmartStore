$.when($.ready).then(function() {
  
});

$("#contact-toggle-input").change(function() {
  var prop = $("#contact-toggle-input").prop("checked");
  var is = $("#contact-toggle-input").is(":checked");
  if (prop || is) {
    $("#contact-search-container").addClass("slds-hidden");
    $("#contact-info").removeClass("slds-is-collapsed")
  } else {
    $("#contact-search-container").removeClass("slds-hidden");
    $("#contact-info").addClass("slds-is-collapsed")
  }
});

$("#username-toggle-input").change(function() {
  var prop = $("#existing-username-toggle").prop("checked");
  var is = $("#existing-username-toggle").is(":checked");
  if (prop || is) {
    $("#username-search-container").removeClass("slds-hidden");
  } else {
    $("#username-search-container").addClass("slds-hidden");
  }
});