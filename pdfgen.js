const fs = require("fs");
const PDFDocument = require("pdfkit");
const md = fs.readFileSync("SLIDE.md","utf8");
const lines = md.split(/\r?\n/);
const doc = new PDFDocument({size:"A4", margin:50});
const out = fs.createWriteStream("PRESENTATION.pdf");
doc.pipe(out);
// Title
let i = 0;
let title = "";
for (; i < lines.length; i++) {
  if (lines[i].trim().length === 0) continue;
  title = lines[i].replace(/^#\s*/,'').trim();
  i++;
  break;
}
doc.font('Helvetica-Bold').fontSize(26).text(title, {align: 'center'});
doc.moveDown(1.5);

doc.font('Helvetica').fontSize(12);
for (; i < lines.length; i++) {
  const line = lines[i];
  if (!line || line.trim().length === 0) { doc.moveDown(0.5); continue; }
  const t = line.trim();
  if (t.startsWith('- ')) {
    doc.text('• ' + t.substring(2));
  } else if (t.endsWith(':')) {
    doc.font('Helvetica-Bold').text(t);
    doc.font('Helvetica').moveDown(0.1);
  } else {
    doc.text(t);
  }
}

doc.end();
out.on('finish', function(){ console.log('PRESENTATION.pdf written'); });
