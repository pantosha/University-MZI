function [encryption, decryptedText] = caesarEncrypt(text, offset)
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

loweredText = lower(text);
decryptedText = loweredText(ismember(loweredText, alphabet));
encryption = decryptedText;
offset = mod(offset + length(alphabet), length(alphabet));
if offset == 0
    return;
end

encryptedAlphabet = [alphabet(offset+1:end); alphabet(1:offset)];
for i = 1:length(alphabet)
    encryption(alphabet(i) == decryptedText) = encryptedAlphabet(i);
end
end