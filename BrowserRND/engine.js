if (!Element.prototype.matches) Element.prototype.matches = Element.prototype.msMatchesSelector;
if (!Element.prototype.closest) Element.prototype.closest = function (selector) {
    var el = this;
    while (el) {
        if (el.matches(selector)) {
            return el;
        }
        el = el.parentElement;
    }
};
var engineHelper = (function (win) {
    var _addStep = function (step) {
        try {
            win.external.PushRecordSteps(JSON.stringify(step));
        } catch (error) {
            console.error(error);
        }
    }
    var _addSteps = function (steps) {
        try {
            win.external.PushAllRecordSteps(JSON.stringify(steps));
        } catch (error) {
            console.error(error);
        }

    }
    var _play = function (s) {
        try {
            var ss = win.external.Play();
        } catch (error) {
            console.error(error);
            return [];
        }
    }
    var _log = function (message) {
        console.log(message);
        try {
            win.external.log(message);
        } catch (error) {
            console.error(error);
        }
    }
    var _showElementSelector = function (message) {
        console.log(message);
        try {
            win.external.ElementSelector(message);
        } catch (error) {
            console.error(error);
        }
    }

    return {
        addStep: _addStep,
        addSteps: _addSteps,
        play: _play,
        log: _log,
        showSelector: _showElementSelector
    }
})(window);

var engine = (function (document, engHelper) {
    var recordActions = [];

    var _clickRecord = function (e) {
        var ss = _getUniqueId(e.target);
        recordActions.push({ action: 'click', selector: ss });
        engHelper.addSteps(recordActions);
    }
    var _submitRecord = function (e) {
        e.preventDefault();
        var ss = _getUniqueId(e.target);
        recordActions.push({ action: 'submit', selector: ss });
        engHelper.addSteps(recordActions);
    }
    var _mouseDownForSubmit = function (e) {
        var action = 'click';
        var ss = _getUniqueId(e.target);
        if (e.target.type === "submit" && e.target.form != undefined) {
            action = 'submit';
            ss = _getUniqueId(e.target.form);
        }
        recordActions.push({ action: action, selector: ss });
        engHelper.addSteps(recordActions);

    }
    var _keydown = function (e) {
        var evtobj = window.event ? event : e
        engHelper.log('Keydown :: ' + evtobj.keyCode);
        if (!evtobj.ctrlKey || !evtobj.altKey)
            return;
        switch (evtobj.keyCode) {
            case 82: _record(); break;
            case 83: _stopRecord(); break;
            case 80: _callPlay(); break;
        }


    }
    var _keyPressRecord = function (e) {
        engHelper.log("_keyPressRecord" + e.target.tagName);
        var ss = _getUniqueId(e.target);
        engHelper.log("_keyPressRecord target " + ss);
        var index = -1;
        for (var i = 0; i < recordActions.length; ++i) {
            if (recordActions[i].action == 'input' && recordActions[i].selector == ss) {
                index = i;
                break;
            }
        } //.findIndex(function (t) { return t. });
        engHelper.log("_keyPressRecord :: " + index);
        if (index == -1)
            recordActions.push({ action: 'input', selector: ss, value: e.target.value });
        else
            recordActions[index].value = e.target.value;
        engHelper.log("_keyPressRecord");
        engHelper.addSteps(recordActions);
    }
    var _record = function () {
        recordActions = [];
        document.addEventListener('keydown', _keyPressRecord);
        document.addEventListener('click', _clickRecord);
        document.addEventListener('mousedown', _mouseDownForSubmit);
        var forms = document.querySelectorAll('form');
        for (let index = 0; index < forms.length; index++) {
            const element = forms[index];
            element.addEventListener('submit', _submitRecord);
        }

    }
    var _stopRecord = function () {
        document.removeEventListener('click', _clickRecord);
        document.removeEventListener('keydown', _keyPressRecord);
        document.removeEventListener('mousedown', _mouseDownForSubmit);
        var forms = document.querySelectorAll('form');
        for (let index = 0; index < forms.length; index++) {
            const element = forms[index];
            element.addEventListener('submit', _submitRecord);
        }
        engHelper.addSteps(_getRecordded());
        engHelper.log('Recorded', _getRecordded());
    }
    var _getRecordded = function () {
        return recordActions;
    }
    var _callPlay = function () {
        engHelper.log("call play action");
        engHelper.play();
    }

    var _playGivenActions = function (actions) {
        engHelper.log(actions);
        recordActions = JSON.parse(actions);
        recordActions.forEach(function (element) {
            engHelper.log(element.action + " :: " + element.selector);
            switch (element.action) {
                case 'submit':
                    setTimeout(function () { document.querySelector(element.selector).submit(); }, 0); break;
                case 'click':
                    setTimeout(function () {
                        engHelper.log(element.action + "2000 :: " + element.selector);
                        window.document.querySelector(element.selector).click();
                    }, 2000); break;
                case 'input':
                    setTimeout(function () { document.querySelector(element.selector).value = element.value; }, 0); break;
            }
        });
    }
    var _getUniqueId = function (e) {
        if (e.tagName == 'HTML' | 'Body')
            return e.tagName;
        var selector = [];
        selector.push(e.tagName);
        selector = selector.concat(_getSelectorArray(e.attributes));
        var uniqueSelector = _getUniqSelector(selector);
        if (typeof uniqueSelector === 'string')
            return uniqueSelector;
        return _getUniqueId(e.parentElement) + ' ' + uniqueSelector[0];
    }
    var _getSelectorArray = function (attributes) {
        var selector = [];
        for (var index = 0; index < attributes.length; index++) {
            const element = attributes[index];
            if (element.name == 'id') {
                selector.push('#' + element.value);
                continue;
            }
            if (element.name == 'class') {
                selector.push('.' + element.value.split(' ').join('.'));
                continue;
            }
            if (element.value)
                selector.push('[' + element.name + '=\'' + element.value + '\']');
            else
                selector.push('[' + element.name + ']');
        }
        return selector;
    }
    var _getUniqSelector = function (stringarray) {
        for (let index = 0; index < stringarray.length; index++) {
            const element = stringarray[index];
            if (document.querySelectorAll(element).length == 1)
                return element;
        }
        var join = stringarray.join('');
        if (document.querySelectorAll(join).length == 1)
            return join;
        else {
            const lastsel = [];
            for (let index = 0; index < document.querySelectorAll(join).length; index++) {
                const element = document.querySelectorAll(join).length[index];
                lastsel.push(join);
            }
            return lastsel;
        }
    }


    var _get = function () {
        return document.querySelector('form input').closest('#q123').innerHTML;
    }

    var _appendData = function () {
        let div = document.createElement('div');
        let style = document.createElement('style');
        div.innerHTML = "<div id='test-auto-too'>\n        <label>[CTRL + ALT + R] = Record</label>\n        <br/>\n        <label> [CTRL + ALT + S] = Stop </label>\n        <br/>\n        <label> [CTRL + ALT + P] = Play </label>\n    </div>";
        style.innerHTML = '#test-auto-too {display: block;position: fixed;opacity: 0.5;bottom: 20px;right: 20px;padding: 20px;} #test-auto-too:hover {opacity: 1;border: 1px solid blue;}';
        if (document.body) {
            document.body.appendChild(div);
            document.body.appendChild(style);
        }
    };
    document.addEventListener('mouseover', function (e) {
        var ss = _getUniqueId(e.target);
        engHelper.showSelector(ss);
    });
    window.addEventListener('load', function (e) {

        if (document.readyState === 'complete') {
            _appendData();
        }

    });
    document.addEventListener('keydown', _keydown);
    _appendData();
    return {
        getHits: _getRecordded,
        play: _playGivenActions,
        record: _record,
        stop: _stopRecord
    }
})(window.document, engineHelper);

function engine_play(actions) {
    engineHelper.log(actions);
    engine.play(actions);
}

