(function () {
  function log(...args) {
    try { console.debug('[swagger-custom]', ...args); } catch { }
  }

  function fillExample() {
    try {
      log('running fillExample');
      var opblocks = document.querySelectorAll('.opblock');
      opblocks.forEach(function (op) {
        var methodEl = op.querySelector('.opblock-summary-method');
        var pathEl = op.querySelector('.opblock-summary-path');
        if (!methodEl || !pathEl) return;
        var method = methodEl.textContent.trim().toUpperCase();
        var path = pathEl.textContent.trim();
        if (method === 'POST' && path === '/api/statements/generate') {
          log('found generate opblock');
          var tryBtn = op.querySelector('.opblock-control .btn.try-out__btn');
          if (tryBtn && tryBtn.textContent.toLowerCase().includes('try it')) tryBtn.click();

          var example = {
            customerId: '1',
            month: 6,
            year: 2026
          };

          var textarea = op.querySelector('textarea');
          if (textarea) {
            log('filling textarea');
            textarea.value = JSON.stringify(example, null, 2);
            var event = new Event('input', { bubbles: true });
            textarea.dispatchEvent(event);
            return;
          }

          var cm = op.querySelector('.CodeMirror');
          if (cm && cm.CodeMirror && typeof cm.CodeMirror.setValue === 'function') {
            log('filling CodeMirror');
            cm.CodeMirror.setValue(JSON.stringify(example, null, 2));
            return;
          }

          var editable = op.querySelector('[contenteditable]');
          if (editable) {
            log('filling contenteditable');
            editable.textContent = JSON.stringify(example, null, 2);
            editable.dispatchEvent(new Event('input', { bubbles: true }));
            return;
          }
        }
      });

      var exampleObj = {
        customerId: '1',
        month: 6,
        year: 2026
      };
      document.querySelectorAll('pre').forEach(function (pre) {
        var text = pre.textContent || '';
        if (text.indexOf('"customerId": "string"') !== -1 || text.indexOf('"month": 0') !== -1) {
          try {
            pre.textContent = JSON.stringify(exampleObj, null, 2);
          } catch (e) {
          }
        }
      });
    } catch (e) {
      log('error in fillExample', e);
    }
  }

  if (document.readyState === 'complete' || document.readyState === 'interactive') {
    setTimeout(fillExample, 500);
  } else {
    document.addEventListener('DOMContentLoaded', function () { setTimeout(fillExample, 500); });
  }

  var tries = 0;
  var interval = setInterval(function () {
    tries++;
    fillExample();
    if (tries > 20) clearInterval(interval);
  }, 500);
})();
