function decryptedText = caesarDecrypt(text)
alphabet = [
    'a'; 'b'; 'c';
    'd'; 'e'; 'f';
    'g'; 'h'; 'i';
    'j'; 'k'; 'l';
    'm'; 'n'; 'o';
    'p'; 'q'; 'r';
    't'; 's'; 'u';
    'v'; 'w'; 'x';
    'y'; 'z'; ' '
];

frequency = [
    0.0651738
    0.0124248
    0.0217339
    0.0349835
    0.1041442
    0.0197881
    0.0158610
    0.0492888
    0.0558094
    0.0009033
    0.0050529
    0.0331490
    0.0202124
    0.0564513
    0.0596302
    0.0137645
    0.0008606
    0.0497563
    0.0515760
    0.0729357
    0.0225134
    0.0082903
    0.0171272
    0.0013692
    0.0145984
    0.0007836
    0.1918182
];
if ~all(ismember(text, alphabet))
    error('Text contains symbols that are not in alphabet.');
end
encryptionLettersCount = sum(bsxfun(@eq, text, alphabet), 2);
encryptionFrequency = encryptionLettersCount./sum(encryptionLettersCount);

chi2 = zeros(1, length(alphabet));
for i = 1:length(alphabet)
    encryptionFrequency = [encryptionFrequency(end); encryptionFrequency(1:end-1)];
    chi2(i) = pearsonTest(encryptionFrequency, frequency);
end
[~, offset] = min(chi2);
decryptedText = caesarEncrypt(text, offset);
end

function chi2 = pearsonTest(actualFrequency, expectedFrequency)
chi2 = sum(((actualFrequency - expectedFrequency).^2)./expectedFrequency);
end